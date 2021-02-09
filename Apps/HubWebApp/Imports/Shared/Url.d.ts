import { UrlHash } from "./UrlHash";
import { UrlQuery } from "./UrlQuery";
export declare class Url {
    static current(): Url;
    private readonly url;
    private readonly _query;
    private readonly _hash;
    constructor(baseUrl: string);
    hasQuery(name: string): boolean;
    readonly query: UrlQuery;
    getQueryValue(name: string): string;
    readonly hash: UrlHash;
    hasHash(name: string): boolean;
    getHashValue(name: string): string;
    value(): string;
    withoutQueryAndHash(): string;
    toString(): string;
}
