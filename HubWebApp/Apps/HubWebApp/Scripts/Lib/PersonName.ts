
export class PersonName {
    readonly value: string;
    readonly displayText: string;

    constructor(source?: IPersonName) {
        this.value = source ? source.Value : "";
        this.displayText = source ? source.DisplayText : "";
    }

    toString() { return this.displayText; }
}