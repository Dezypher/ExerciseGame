

function DrawGameOverlay() {
    var time = Math.ceil(currSeconds);

    // Print Points
    var pointText = points + " POINTS (" + amtDone + "/" + doAmt + ")";

    // Print Time Left

    var timeLeftText = "";

	if (canGetPoint)
		timeLeftText = "Time Left: " + time;
	else
		timeLeftText = "";

    // Main Text Prompt in the Middle
    var promptText = "";

    if (done) {
		if (started) {
			if (!resting && canGetPoint) {
				promptText = "GO!";
			} else if (success) {
				promptText = "GOOD JOB!";
			} else if (failed) {
				promptText = "TOO BAD!";
			}
			
            if (time <= 3)
				promptText = "" + time;
		} else {
			promptText = "READY";
				if (time <= 3)
				promptText = "" + time;
		}
	} else {
		promptText = "FINISHED!";
	}

    // PRINT PROMPT TEXT

    // Rating with Stars
}