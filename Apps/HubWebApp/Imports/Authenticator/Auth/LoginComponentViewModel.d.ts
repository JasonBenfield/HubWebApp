import { OffscreenSubmitViewModel } from 'XtiShared/OffscreenSubmitViewModel';
import * as ko from 'knockout';
import { AlertViewModel } from 'XtiShared/Alert';
import { VerifyLoginFormViewModel } from "../Api/VerifyLoginFormViewModel";
import { AuthenticatorAppApi } from "../Api/AuthenticatorAppApi";
export declare class LoginComponentViewModel {
    constructor(authApi: AuthenticatorAppApi);
    readonly componentName: ko.Observable<string>;
    readonly alert: AlertViewModel;
    readonly verifyLoginForm: VerifyLoginFormViewModel;
    readonly loginCommand: import("../../../Imports/Shared/Command").CommandViewModel;
    readonly offscreenSubmit: OffscreenSubmitViewModel;
}
