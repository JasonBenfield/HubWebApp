import { Block } from "@jasonbenfield/sharedwebapp/Html/Block";
import { Container } from "@jasonbenfield/sharedwebapp/Html/Container";
import { FlexColumn } from '@jasonbenfield/sharedwebapp/Html/FlexColumn';
import { FlexColumnFill } from '@jasonbenfield/sharedwebapp/Html/FlexColumnFill';
import { TextHeading1 } from "@jasonbenfield/sharedwebapp/Html/TextHeading1";
import { PaddingCss } from "@jasonbenfield/sharedwebapp/PaddingCss";
import { PageFrameView } from '@jasonbenfield/sharedwebapp/PageFrameView';
import { Startup } from '@jasonbenfield/sharedwebapp/Startup';
import { LoginComponentView } from "./LoginComponentView";

export class LoginPageView {
    readonly loginComponent: LoginComponentView;

    constructor(private readonly page: PageFrameView) {
        let flexColumn = this.page.addContent(new FlexColumn());
        flexColumn
            .addContent(new Block())
            .addContent(new Container())
            .addContent(new TextHeading1('Login'));
        this.loginComponent = flexColumn
            .addContent(new FlexColumnFill())
            .container
            .configure(c => c.setPadding(PaddingCss.top(3)))
            .addContent(new LoginComponentView());
    }
}