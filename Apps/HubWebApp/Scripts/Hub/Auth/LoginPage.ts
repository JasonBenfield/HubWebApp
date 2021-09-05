import { Block } from "XtiShared/Html/Block";
import { Container } from "XtiShared/Html/Container";
import { FlexColumn } from 'XtiShared/Html/FlexColumn';
import { FlexColumnFill } from 'XtiShared/Html/FlexColumnFill';
import { TextHeading1 } from "XtiShared/Html/TextHeading1";
import { PaddingCss } from "XtiShared/PaddingCss";
import { PageFrame } from 'XtiShared/PageFrame';
import { Startup } from 'xtistart';
import { HubAppApi } from "../Api/HubAppApi";
import { LoginComponent } from "./LoginComponent";

class LoginPage {
    constructor(private readonly page: PageFrame) {
        let flexColumn = this.page.addContent(new FlexColumn());
        flexColumn
            .addContent(new Block())
            .addContent(new Container())
            .addContent(new TextHeading1('Login'));
        flexColumn
            .addContent(new FlexColumnFill())
            .container
            .configure(c => c.setPadding(PaddingCss.top(3)))
            .addContent(new LoginComponent(this.hubApi));
    }

    private readonly hubApi = this.page.api(HubAppApi);
}
new LoginPage(new Startup().build());