function changeFreq(freq) {
    var frequency = document.getElementById('frequency');
    var duration = document.getElementById('duration');
    var durationunit = document.getElementById('durationunit');
    var totalTrials = document.getElementById('totalTrials');
    var successfulTrials = document.getElementById('successfulTrials');
    switch (freq) {
        case "Frequency":
            frequency.removeAttribute("class", "d-none");
            duration.setAttribute("class", "d-none");
            durationunit.setAttribute("class", "d-none");
            totalTrials.setAttribute("class", "d-none");
            successfulTrials.setAttribute("class", "d-none");
            frequency.setAttribute("class", "form-group");
            break;
        case "Trials":
            totalTrials.removeAttribute("class", "d-none");
            successfulTrials.removeAttribute("class", "d-none");
            totalTrials.setAttribute("class", "form-group");
            successfulTrials.setAttribute("class", "form-group");
            durationunit.setAttribute("class", "d-none");
            frequency.setAttribute("class", "d-none");
            break;
        case "Duration":
            frequency.removeAttribute("class", "d-none");
            frequency.setAttribute("class", "form-group");
            duration.removeAttribute("class", "d-none");
            duration.setAttribute("class", "form-group");
            durationunit.removeAttribute("class", "d-none");
            durationunit.setAttribute("class", "form-group");
            totalTrials.setAttribute("class", "d-none");
            successfulTrials.setAttribute("class", "d-none");
            break;
    }

}