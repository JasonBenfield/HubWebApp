
export class AppCommandName {
    readonly value: string;

    constructor(source?: IAppCommandName) {
        this.value = source ? source.Value : "";
    }
}