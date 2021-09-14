import { Url } from "./Url";
import { UrlBuilder } from "./UrlBuilder";
export declare class WebPage {
    constructor(url: string | UrlBuilder | Url);
    private readonly url;
    open(): void;
    transfer(): void;
    openWindow(): void;
    openForPrint(): void;
}
