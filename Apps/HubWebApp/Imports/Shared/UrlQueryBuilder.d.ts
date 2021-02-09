import { NamedValue } from "./NamedValue";
import { UrlQuery } from "./UrlQuery";
export declare class UrlQueryBuilder {
    private _query;
    constructor(query: string | UrlQuery);
    readonly query: UrlQuery;
    getValues(): NamedValue[];
    getValue(name: string): string;
    clear(): this;
    hasQuery(name: string): boolean;
    removeQuery(name: string): this;
    replaceQuery(name: string, value: string[]): UrlQueryBuilder;
    replaceQuery(name: string, value: string): UrlQueryBuilder;
    replaceQuery(name: string, value: Date): UrlQueryBuilder;
    replaceQuery(name: string, value: number): UrlQueryBuilder;
    addQuery(name: string, value: string[]): UrlQueryBuilder;
    addQuery(name: string, value: string): UrlQueryBuilder;
    addQuery(name: string, value: Date): UrlQueryBuilder;
    addQuery(name: string, value: number): UrlQueryBuilder;
    addQueryFromObject(obj: any): void;
    private _addQueryFromObject;
    addQueryString(query: string): this;
    toString(): string;
}
