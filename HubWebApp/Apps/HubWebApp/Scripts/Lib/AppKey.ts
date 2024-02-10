import { AppName } from "./AppName";
import { AppType } from "./Http/AppType";

export class AppKey {
    readonly name: AppName;
    readonly type: AppType;

    constructor(source: IAppKey) {
        this.name = new AppName(source.Name);
        this.type = AppType.values.value(source.Type);
    }

    format() {
        return `${this.name.displayText} ${this.type.DisplayText}`;
    }

    toString() { return this.format(); }
}