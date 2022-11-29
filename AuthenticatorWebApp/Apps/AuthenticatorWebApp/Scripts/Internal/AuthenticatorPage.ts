import { BasicPage } from "@jasonbenfield/sharedwebapp/Components/BasicPage";
import { AuthenticatorAppApi } from "../Lib/Api/AuthenticatorAppApi";
import { Apis } from "./Apis";
import { AuthenticatorPageView } from "./AuthenticatorPageView";

export class AuthenticatorPage extends BasicPage {
    protected readonly defaultApp: AuthenticatorAppApi;

    constructor(view: AuthenticatorPageView) {
        super(new Apis(view.modalError).Authenticator(), view);
    }
}