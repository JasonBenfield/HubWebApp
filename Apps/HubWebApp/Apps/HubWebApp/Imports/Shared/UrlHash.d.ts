import { NamedValue } from "./NamedValue";
export declare class UrlHash {
    constructor(hash: string | NamedValue[]);
    private pushHashValues;
    private readonly hashValues;
    getValues(): NamedValue[];
    getValue(name: string): string;
    clear(): this;
    hasQuery(name: string): boolean;
    toString(): string;
}
