var creepyThings;
var filteredThings;

$(document).ready(function () {
    $.getJSON('http://creepedout.azurewebsites.net/odata/CreepyThings', function (data) {
        creepyThings = data.value;
        for (var i = 0; i < creepyThings.length; i++) {
            creepyThings[i].DocContentToLower = creepyThings[i].DocContent.toLowerCase();
            creepyThings[i].hits = 0;
            creepyThings[i].percentMatch = 1;
        }
        updateMap(data);
    });
});


$('#search-text').on('input', function () {
    if (creepyThings) {

        var text = $('#search-text').val()
        var words = text.split(',').map(function (a) {
            return a.replace(' ', '').toLowerCase();
        }).filter(function (word) { return word != ''; });

        filteredThings = creepyThings.filter(function (creepyThing) {
            creepyThing.hits = 0;
            if (words.length > 0) {
                for (var i = 0; i < words.length; i++) {
                    if (words[i] != '') {
                        if (creepyThing.DocContentToLower.indexOf(words[i]) >= 0) {
                            creepyThing.hits += 1;
                        }
                    }
                }
                creepyThing.percentMatch = creepyThing.hits / words.length;
            } else {
                creepyThing.percentMatch = 1;
            }
            return creepyThing.percentMatch > 0;
        });

        updateMap({ 'value': filteredThings });
    }
});


function updateMap(data) {

    var settime = function (globe, t) {
        return function () {
            new TWEEN.Tween(globe).to({ time: t / years.length }, 500).easing(TWEEN.Easing.Cubic.EaseOut).start();
            var y = document.getElementById('year' + years[t]);
            if (y.getAttribute('class') === 'year active') {
                return;
            }
            var yy = document.getElementsByClassName('year');
            for (i = 0; i < yy.length; i++) {
                yy[i].setAttribute('class', 'year');
            }
            y.setAttribute('class', 'year active');
        };
    };

    var lerp = function (a, b, t) {
        return a + t * (b - a);
    };

    window.globe.reset();
    var value = 0;
    var arry = [];
    var colorArryFactor = [];
    var group = {};
    var maxGroupCount = 0;
    for (i = 0; i < data.value.length; i++) {
        if (data.value[i].Latitude) {
            var key = data.value[i].Latitude + '|' + data.value[i].Longitude;
            if (!group[key]) {
                value = Math.min(value + 0.1, 1);
                group[key] = { 'lat': data.value[i].Latitude, 'lng': data.value[i].Longitude, 'value': value, 'itemCount': 1, 'hitPercentMatchTotal': 0 };
            }
            else {
                group[key].value += Math.random();
                group[key].itemCount += 1;
            }
            group[key].hitPercentMatchTotal += data.value[i].percentMatch;

            maxGroupCount = Math.max(maxGroupCount, group[key].itemCount);
        }
    }
    console.log('maxGroupCount: ' + maxGroupCount);
    //loop through the grouped items and format them
    for (key in group) {
        if (group.hasOwnProperty(key)) {
            var percentMatch = group[key].hitPercentMatchTotal / group[key].itemCount;
            arry.push(group[key].lat);
            arry.push(group[key].lng);
            arry.push(group[key].itemCount / maxGroupCount);
            colorArryFactor.push({ 'r': lerp(1, 0, percentMatch), 'g': lerp(0, 1, percentMatch), 'b': 0 })
            // arry.push(data.value[i].Latitude);
            // arry.push(data.value[i].Longitude);
            // arry.push((Math.random() ));
        }
    }

    var formattedData = [["sample", arry]];
    //window.data = formattedData;
    for (i = 0; i < formattedData.length; i++) {
        window.globe.addData(formattedData[i][1], { format: 'magnitude', name: formattedData[i][0], animated: false, color: colorArryFactor });
    }
    window.globe.createPoints();
    settime(window.globe, 0)();
    window.globe.animate();
    document.body.style.backgroundImage = 'none'; // remove loading
}