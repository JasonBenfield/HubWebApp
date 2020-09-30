import { JoinedStrings } from "./JoinedStrings";

export class XtiPath {
    static app(appKey: string, version: string) { return new XtiPath(appKey, version, null, null); }

    private constructor(
        public readonly app: string,
        public readonly version: string,
        public readonly group: string,
        public readonly action: string
    ) {
        let parts = [this.app, this.version];
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
        return new XtiPath(this.app, this.version, group, null);
    }

    withAction(action: string) {
        return new XtiPath(this.app, this.version, this.group, action);
    }

    format() {
        return this.value;
    }

    equals(other: XtiPath) {
        if (other) {
            return this.value === other.value;
        }
        return false;
    }

    toString() {
        return this.value;
    }
}