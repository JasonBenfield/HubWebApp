import { BasicPageView } from '@jasonbenfield/sharedwebapp/Views/BasicPageView';
import { UserGroupsPanelView } from './UserGroupsPanelView';

export class MainPageView extends BasicPageView {
    readonly userGroupsPanel: UserGroupsPanelView;

    constructor() {
        super();
        this.userGroupsPanel = this.addView(UserGroupsPanelView);
    }
}