import { NamedValue } from "./NamedValue";
import { UrlHash } from "./UrlHash";
export declare class UrlHashBuilder {
    private _hash;
    constructor(hash: string | UrlHash);
    get hash(): UrlHash;
    getValues(): NamedValue[];
    getValue(name: string): string;
    clear(): this;
    hasQuery(name: string): boolean;
    removeQuery(name: string): this;
    replaceQuery(name: string, value: string[]): UrlHashBuilder;
    replaceQuery(name: string, value: string): UrlHashBuilder;
    replaceQuery(name: string, value: Date): UrlHashBuilder;
    replaceQuery(name: string, value: number): UrlHashBuilder;
    addQuery(name: string, value: string[]): UrlHashBuilder;
    addQuery(name: string, value: string): UrlHashBuilder;
    addQuery(name: string, value: Date): UrlHashBuilder;
    addQuery(name: string, value: number): UrlHashBuilder;
    addQueryFromObject(obj: any): void;
    private _addQueryFromObject;
    addQueryString(query: string): this;
    toString(): string;
}
