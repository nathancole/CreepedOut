var creepyThings;
var filteredThings;

$(document).ready(function () {
    $.getJSON('http://creepedout.azurewebsites.net/odata/CreepyThings', function (data) {
        creepyThings = data.value;
    });
});


$('#search-text').on('input', function () {
    var text = $('#search-text').val()
    var words = text.split(',').map(function (a) {
        return a.trim();
    });
    
    filteredThings = creepyThings.filter(function (creepyThing) {
        creepyThing.hits = 0;
        for (var i = 0; i < words.length; i++) {
            if (creepyThing.DocContent.indexOf(words[i]) >= 0) {
                creepyThing.hits += 1;
            };
        }
        creepyThing.percentMatch = creepyThing.hits / words.length;
        return creepyThing.hits > 0;
    });

    updateMap(filteredThings);

});


function updateMap(filteredThings) {

}