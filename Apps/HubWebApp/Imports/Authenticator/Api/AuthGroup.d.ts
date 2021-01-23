import { AppApiGroup } from "XtiShared/AppApiGroup";
import { AppApiAction } from "XtiShared/AppApiAction";
import { AppApiView } from "XtiShared/AppApiView";
import { AppApiEvents } from "XtiShared/AppApiEvents";
import { AppResourceUrl } from "XtiShared/AppResourceUrl";
import { VerifyLoginForm } from "./VerifyLoginForm";
export declare class AuthGroup extends AppApiGroup {
    constructor(events: AppApiEvents, resourceUrl: AppResourceUrl);
    readonly Index: AppApiView<IEmptyRequest>;
    readonly VerifyLoginAction: AppApiAction<VerifyLoginForm, IEmptyActionResult>;
    readonly VerifyLoginForm: AppApiView<IEmptyRequest>;
    readonly Login: AppApiView<ILoginModel>;
    readonly Logout: AppApiView<IEmptyRequest>;
    VerifyLogin(model: VerifyLoginForm, errorOptions?: IActionErrorOptions): Promise<IEmptyActionResult>;
}
