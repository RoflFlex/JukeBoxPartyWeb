﻿var audio = document.createElement('audio');

// Add a change event listener to the file input
document.getElementById("userFile").addEventListener('change', function (event) {
    var target = event.currentTarget;
    var file = target.files[0];
    var reader = new FileReader();

    if (target.files && file) {
        var reader = new FileReader();

        reader.onload = function (e) {
            audio.src = e.target.result;
            audio.addEventListener('loadedmetadata', function () {
                // Obtain the duration in seconds of the audio file (with milliseconds as well, a float value)
               // var duration = (audio.duration * 100).toString();
                var duration = audio.duration ;
                // example 12.3234 seconds
                console.log("The duration of the song is of: " + duration + " seconds");
               // var remove_after = duration.indexOf('.');
               // var result = x.substring(0, remove_after);
                document.getElementById("duration").value = duration;
                // Alternatively, just display the integer value with
                // parseInt(duration)
                // 12 seconds
            }, false);
        };

        reader.readAsDataURL(file);
    }
}, false); 