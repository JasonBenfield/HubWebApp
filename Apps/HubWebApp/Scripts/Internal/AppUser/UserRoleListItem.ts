import { DefaultEvent } from "@jasonbenfield/sharedwebapp/Events";
import { TextBlock } from "@jasonbenfield/sharedwebapp/Html/TextBlock";
import { UserRoleListItemView } from "./UserRoleListItemView";

export class UserRoleListItem {
    private readonly _deleteButtonClicked = new DefaultEvent<IAppRoleModel>(this);
    readonly deleteButtonClicked = this._deleteButtonClicked.handler();

    constructor(private readonly role: IAppRoleModel, private readonly view: UserRoleListItemView) {
        new TextBlock(role.Name, view.roleName);
        view.deleteButton.clicked.register(this.onDeleteButtonClicked.bind(this), this);
    }

    private onDeleteButtonClicked() {
        this._deleteButtonClicked.invoke(this.role);
    }

    dispose() {
        this.view.deleteButton.clicked.unregister(this);
    }
}