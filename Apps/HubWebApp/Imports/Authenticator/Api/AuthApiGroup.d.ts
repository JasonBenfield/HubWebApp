import { AppApiGroup } from "XtiShared/AppApiGroup";
import { AppApiAction } from "XtiShared/AppApiAction";
import { AppApiEvents } from "XtiShared/AppApiEvents";
import { AppResourceUrl } from "XtiShared/AppResourceUrl";
export declare class AuthApiGroup extends AppApiGroup {
    constructor(events: AppApiEvents, resourceUrl: AppResourceUrl);
    readonly AuthenticateAction: AppApiAction<ILoginCredentials, ILoginResult>;
    Authenticate(model: ILoginCredentials, errorOptions?: IActionErrorOptions): Promise<ILoginResult>;
}
