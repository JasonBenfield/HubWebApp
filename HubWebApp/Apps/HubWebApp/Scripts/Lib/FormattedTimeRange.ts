import { DateTimeOffset } from "@jasonbenfield/sharedwebapp/DateTimeOffset";

export class FormattedTimeRange {
    constructor(readonly timeStarted: DateTimeOffset, readonly timeEnded: DateTimeOffset) {
    }

    format() {
        let timeRange: string;
        const timeStarted = this.timeStarted.format();
        if (this.timeEnded.isMaxYear) {
            timeRange = `${timeStarted} to ???`;
        }
        else {
            let timeEnded: string;
            if (this.timeStarted.toDateOnly().equals(this.timeEnded.toDateOnly())) {
                timeEnded = this.timeEnded.formatTime();
            }
            else {
                timeEnded = this.timeEnded.format();
            }
            const ts = this.timeEnded.minus(this.timeStarted);
            timeRange = `${timeStarted} to ${timeEnded} [ ${ts} ]`;
        }
        return timeRange;
    }

    toString() { return this.format(); }
}