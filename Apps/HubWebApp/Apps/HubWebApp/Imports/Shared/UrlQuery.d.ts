import { NamedValue } from "./NamedValue";
export declare class UrlQuery {
    constructor(query: string | NamedValue[]);
    private readonly queryValues;
    getValues(): NamedValue[];
    getValue(name: string): string;
    private pushQueryValues;
    hasQuery(name: string): boolean;
    toString(): string;
}
