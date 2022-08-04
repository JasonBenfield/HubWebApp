import { BasicPage } from '@jasonbenfield/sharedwebapp/Components/BasicPage';
import { TextComponent } from '@jasonbenfield/sharedwebapp/Components/TextComponent';
import { MainPageView } from './MainPageView';

declare let serverError: IErrorModel;

class MainPage extends BasicPage {
    protected readonly view: MainPageView;

    constructor() {
        super(new MainPageView());
        new TextComponent(this.view.caption).setText(serverError.Caption);
        new TextComponent(this.view.message).setText(serverError.Message);
    }
}
new MainPage();