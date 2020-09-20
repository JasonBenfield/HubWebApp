import { JoinedStrings } from "./JoinedStrings";

export class AppResourceName {
    static app(appKey: string) { return new AppResourceName(appKey, null, null); }

    private constructor(
        public readonly app: string,
        public readonly group: string,
        public readonly action: string
    ) {
        let parts = [this.app];
        if (this.group) {
            parts.push(this.group);
            if (this.action) {
                parts.push(this.action);
            }
        }
        this.value = new JoinedStrings('/', parts).value();
    }

    private readonly value: string;

    withGroup(group: string) {
        return new AppResourceName(this.app, group, null);
    }

    withAction(action: string) {
        return new AppResourceName(this.app, this.group, action);
    }

    format() {
        return this.value;
    }

    equals(other: AppResourceName) {
        if (other) {
            return this.value === other.value;
        }
        return false;
    }

    toString() {
        return this.value;
    }
}