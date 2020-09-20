import { AppApiGroup } from "./AppApiGroup";
import { AppApiEvents } from "./AppApiEvents";
import { AppResourceUrl } from "./AppResourceUrl";
import { UserGroup } from './UserGroup';

export class AppApi {
    constructor(
        private readonly events: AppApiEvents,
        baseUrl: string,
        app: string
    ) {
        this.resourceUrl = AppResourceUrl.app(baseUrl, app, pageContext.CacheBust);
        this.addGroup((evts, ru) => new UserGroup(evts, ru));
    }

    private readonly resourceUrl: AppResourceUrl;

    get name() { return this.resourceUrl.resourceName.app; }

    get url() { return this.resourceUrl.relativeUrl; }

    readonly groups: {
        [name: string]: AppApiGroup
    } = {};

    readonly User: UserGroup;

    protected addGroup<T extends AppApiGroup>(
        createGroup: (evts: AppApiEvents, resourceUrl: AppResourceUrl) => T
    ) {
        let group = createGroup(this.events, this.resourceUrl);
        this.groups[group.name] = group;
        return group;
    }

    toString() {
        return `AppApi ${this.resourceUrl}`;
    }
}