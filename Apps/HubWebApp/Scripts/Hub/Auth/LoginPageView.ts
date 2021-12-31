import { Block } from "@jasonbenfield/sharedwebapp/Html/Block";
import { Container } from "@jasonbenfield/sharedwebapp/Html/Container";
import { FlexColumn } from '@jasonbenfield/sharedwebapp/Html/FlexColumn';
import { FlexColumnFill } from '@jasonbenfield/sharedwebapp/Html/FlexColumnFill';
import { TextHeading1View } from "@jasonbenfield/sharedwebapp/Html/TextHeading1View";
import { PaddingCss } from "@jasonbenfield/sharedwebapp/PaddingCss";
import { PageFrameView } from '@jasonbenfield/sharedwebapp/PageFrameView';
import { LoginComponentView } from "./LoginComponentView";

export class LoginPageView {
    readonly loginComponent: LoginComponentView;

    constructor(private readonly page: PageFrameView) {
        let flexColumn = this.page.addContent(new FlexColumn());
        flexColumn
            .addContent(new Block())
            .addContent(new Container())
            .addContent(new TextHeading1View())
            .configure(th => th.setText('Login'));
        this.loginComponent = flexColumn
            .addContent(new FlexColumnFill())
            .container
            .configure(c => c.setPadding(PaddingCss.top(3)))
            .addContent(new LoginComponentView());
    }
}