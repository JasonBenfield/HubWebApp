import { UserListItemView } from "./UserListItemView";

export class UserListItem {
    constructor(readonly user: IAppUserModel, view: UserListItemView) {
        view.setUserName(user.UserName);
        view.setFullName(user.Name);
    }
}
