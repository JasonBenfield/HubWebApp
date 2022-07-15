import { BasicPageView } from '../../../../../../../SharedWebApp/Apps/SharedWebApp/Scripts/Lib/Views/BasicPageView';
import { AddRolePanelView } from './AddRolePanelView';
import { AppUserDataPanelView } from './AppUserDataPanelView';
import { SelectModCategoryPanelView } from './SelectModCategoryPanelView';
import { SelectModifierPanelView } from './SelectModifierPanelView';
import { UserRolesPanelView } from './UserRolesPanelView';

export class MainPageView extends BasicPageView {
    readonly appUserDataPanel: AppUserDataPanelView;
    readonly selectModCategoryPanel: SelectModCategoryPanelView;
    readonly selectModifierPanel: SelectModifierPanelView;
    readonly userRolesPanel: UserRolesPanelView;
    readonly addRolePanel: AddRolePanelView;

    constructor() {
        super();
        this.appUserDataPanel = this.addView(AppUserDataPanelView);
        this.selectModCategoryPanel = this.addView(SelectModCategoryPanelView);
        this.selectModifierPanel = this.addView(SelectModifierPanelView);
        this.userRolesPanel = this.addView(UserRolesPanelView);
        this.addRolePanel = this.addView(AddRolePanelView);
    }
}