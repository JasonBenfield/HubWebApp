import { PaddingCss } from '@jasonbenfield/sharedwebapp/PaddingCss';
import { PageFrameView } from '@jasonbenfield/sharedwebapp/PageFrameView';
import { AppListPanelView } from './AppListPanelView';

export class MainPageView {
    readonly appListPanel: AppListPanelView;

    constructor(private readonly page: PageFrameView) {
        this.appListPanel = this.page.content.addContent(new AppListPanelView());
        this.page.content.setPadding(PaddingCss.top(3));
    }
}