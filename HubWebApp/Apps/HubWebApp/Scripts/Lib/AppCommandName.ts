
export class AppCommandName {
    readonly value: string;

    constructor(source?: IAppCommandName) {
        this.value = source ? source.Value : "";
    }

    get isInstall() { return this.value === "Install"; }

    get isDelete() { return this.value === "Delete"; }

    toString() { return this.value; }
}