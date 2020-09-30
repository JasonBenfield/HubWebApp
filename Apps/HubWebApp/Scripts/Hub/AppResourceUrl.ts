import { XtiPath } from "./XtiPath";
import { UrlBuilder } from "./UrlBuilder";

export class AppResourceUrl {
    static app(baseUrl: string, appKey: string, version: string, cacheBust: string) {
        return new AppResourceUrl(baseUrl, XtiPath.app(appKey, version), cacheBust);
    }

    private constructor(
        private readonly baseUrl: string,
        readonly resourceName: XtiPath,
        private readonly cacheBust: string
    ) {
        this.url = new UrlBuilder(baseUrl)
            .addPart(resourceName.format());
        this.url.addQuery('cacheBust', cacheBust);
    }

    readonly url: UrlBuilder;

    get relativeUrl() {
        return new UrlBuilder(`/${this.resourceName.format()}`);
    }

    withGroup(group: string) {
        return new AppResourceUrl(this.baseUrl, this.resourceName.withGroup(group), this.cacheBust);
    }

    withAction(action: string) {
        return new AppResourceUrl(this.baseUrl, this.resourceName.withAction(action), this.cacheBust);
    }

    toString() {
        return this.url.getUrl();
    }
}