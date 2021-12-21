import { PaddingCss } from '@jasonbenfield/sharedwebapp/PaddingCss';
import { PageFrameView } from '@jasonbenfield/sharedwebapp/PageFrameView';
import { AppUserPanelView } from './AppUser/AppUserPanelView';
import { UserRolePanelView } from './UserRoles/UserRolePanelView';

export class MainPageView {
    readonly appUserPanel = this.page.addContent(new AppUserPanelView());
    readonly userRolePanel = this.page.addContent(new UserRolePanelView());

    constructor(private readonly page: PageFrameView) {
        this.page.content.setPadding(PaddingCss.top(3));
    }
}