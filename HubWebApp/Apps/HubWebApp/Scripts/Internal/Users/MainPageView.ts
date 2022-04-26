import { PaddingCss } from '@jasonbenfield/sharedwebapp/PaddingCss';
import { PageFrameView } from '@jasonbenfield/sharedwebapp/PageFrameView';
import { UserPanelView } from './User/UserPanelView';
import { UserEditPanelView } from './UserEdit/UserEditPanelView';
import { UserListPanelView } from './UserList/UserListPanelView';

export class MainPageView {
    readonly userListPanel = this.page.addContent(new UserListPanelView());
    readonly userPanel = this.page.addContent(new UserPanelView());
    readonly userEditPanel = this.page.addContent(new UserEditPanelView());

    constructor(private readonly page: PageFrameView) {
        this.page.content.setPadding(PaddingCss.top(3));
    }
}