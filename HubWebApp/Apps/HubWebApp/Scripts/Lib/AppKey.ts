import { AppName } from "./AppName";
import { AppType } from "./Http/AppType";

export class AppKey {
    readonly name: AppName;
    readonly type: AppType;

    constructor(source?: IAppKey) {
        this.name = new AppName(source && source.Name);
        this.type = source ? AppType.values.value(source.Type) : AppType.values.NotFound;
    }

    format() {
        return `${this.name.displayText} ${this.type.DisplayText}`;
    }

    toString() { return this.format(); }
}