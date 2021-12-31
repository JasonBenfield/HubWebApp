import { PaddingCss } from '@jasonbenfield/sharedwebapp/PaddingCss';
import { PageFrameView } from '@jasonbenfield/sharedwebapp/PageFrameView';
import { AddRolePanelView } from './AddRolePanelView';
import { AppUserDataPanelView } from './AppUserDataPanelView';
import { SelectModCategoryPanelView } from './SelectModCategoryPanelView';
import { SelectModifierPanelView } from './SelectModifierPanelView';
import { UserRolesPanelView } from './UserRolesPanelView';

export class MainPageView {
    readonly appUserDataPanel: AppUserDataPanelView;
    readonly selectModCategoryPanel: SelectModCategoryPanelView;
    readonly selectModifierPanel: SelectModifierPanelView;
    readonly userRolesPanel: UserRolesPanelView;
    readonly addRolePanel: AddRolePanelView;

    constructor(private readonly page: PageFrameView) {
        this.page.content.setPadding(PaddingCss.top(3));
        this.appUserDataPanel = this.page.addContent(new AppUserDataPanelView());
        this.selectModCategoryPanel = this.page.addContent(new SelectModCategoryPanelView());
        this.selectModifierPanel = this.page.addContent(new SelectModifierPanelView());
        this.userRolesPanel = this.page.addContent(new UserRolesPanelView());
        this.addRolePanel = this.page.addContent(new AddRolePanelView());
    }
}