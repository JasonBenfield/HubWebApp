
export class AppName {
    readonly value: string;
    readonly displayText: string;

    constructor(source?: IAppName) {
        this.value = source?.Value || "";
        this.displayText = source?.DisplayText || "";
    }

    equals(other: AppName | string) {
        if (typeof other === "string") {
            return this.value == other;
        }
        return this.value == other.value;
    }

    toString() { return this.displayText; }
}