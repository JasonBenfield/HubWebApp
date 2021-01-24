import { AppApi } from "XtiShared/AppApi";
import { AppApiEvents } from "XtiShared/AppApiEvents";
import { UserGroup } from "./UserGroup";
import { AuthGroup } from "./AuthGroup";
import { AuthApiGroup } from "./AuthApiGroup";
export declare class AuthenticatorAppApi extends AppApi {
    static readonly DefaultVersion = "V3";
    constructor(events: AppApiEvents, baseUrl: string, version?: string);
    readonly User: UserGroup;
    readonly Auth: AuthGroup;
    readonly AuthApi: AuthApiGroup;
}
