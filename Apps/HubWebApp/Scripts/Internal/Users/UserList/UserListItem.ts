import { TextBlock } from "@jasonbenfield/sharedwebapp/Html/TextBlock";
import { UserListItemView } from "./UserListItemView";

export class UserListItem {
    constructor(readonly user: IAppUserModel, view: UserListItemView) {
        new TextBlock(user.UserName, view.userName);
        new TextBlock(user.Name, view.fullName);
    }
}
