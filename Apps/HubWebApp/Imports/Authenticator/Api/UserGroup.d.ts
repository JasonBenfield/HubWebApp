import { AppApiGroup } from "XtiShared/AppApiGroup";
import { AppApiView } from "XtiShared/AppApiView";
import { AppApiEvents } from "XtiShared/AppApiEvents";
import { AppResourceUrl } from "XtiShared/AppResourceUrl";
export declare class UserGroup extends AppApiGroup implements IUserGroup {
    constructor(events: AppApiEvents, resourceUrl: AppResourceUrl);
    readonly Index: AppApiView<IUserStartRequest>;
}
