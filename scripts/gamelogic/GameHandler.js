var done = false;
var points;
var totalPoints;

var amtSeconds;
var currSeconds;
var interval;

var doAmt;
var amtDone = 0;

var stars;

var resting = false;
var canGetPoint = true;

var success = false;
var failed = false;

var started = false;

var inputDebugWin = false;

var secondsIncrement = 16.666666667;

function CheckValues () {
    currSeconds += secondsIncrement;

	if (amtDone == doAmt) {
			done = true;
	}

	if (!done) {
		if (inputDebugWin && canGetPoint) {
			points++;
			success = true;
			canGetPoint = false;
			currSeconds = 0;
	    }

		if (currSeconds <= 0) {
			if (!resting) {
				if (!success)
					failed = true;
					resting = true;
					canGetPoint = false;
					currSeconds = interval;
			} else {
				if (!started)
						started = true;
					success = false;
					failed = false;
					resting = false;
					canGetPoint = true;
					currSeconds = amtSeconds;
					amtDone++;
				}
			}
		}
	}