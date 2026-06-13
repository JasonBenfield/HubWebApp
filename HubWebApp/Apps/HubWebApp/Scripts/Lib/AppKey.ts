import { AppName } from "./AppName";
import { AppType } from "./Http/AppType";

export class AppKey {
    readonly name: AppName;
    readonly type: AppType;

    constructor(source?: IAppKey) {
        this.name = new AppName(source && source.Name);
        this.type = AppType.values.value(source?.Type.Value || 0);
    }

    get isWebApp() { return this.type.equals(AppType.values.WebApp); }

    get isServiceApp() { return this.type.equals(AppType.values.ServiceApp); }

    get isConsoleApp() { return this.type.equals(AppType.values.ConsoleApp); }

    format() {
        return `${this.name.displayText} ${this.type.DisplayText}`;
    }

    equals(other: AppKey) {
        return this.name.equals(other.name) && this.type.equals(other.type);
    }

    toString() { return this.format(); }
}