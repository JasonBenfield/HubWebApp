
export class PersonName {
    readonly value: string;
    readonly displayText: string;

    constructor(source: IPersonName) {
        this.value = source.Value;
        this.displayText = source.DisplayText;
    }

    toString() { return this.displayText; }
}