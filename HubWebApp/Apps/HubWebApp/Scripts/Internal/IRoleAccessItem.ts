import { AppRole } from "../Lib/AppRole";

export interface IRoleAccessItem {
    readonly isAllowed: boolean;
    readonly role: AppRole;
}