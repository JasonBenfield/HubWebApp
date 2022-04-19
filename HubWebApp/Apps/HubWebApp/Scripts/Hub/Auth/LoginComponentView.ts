import { ColumnCss } from "@jasonbenfield/sharedwebapp/ColumnCss";
import { ButtonCommandItem } from "@jasonbenfield/sharedwebapp/Command/ButtonCommandItem";
import { ContextualClass } from "@jasonbenfield/sharedwebapp/ContextualClass";
import { DelayedAction } from '@jasonbenfield/sharedwebapp/DelayedAction';
import { Block } from "@jasonbenfield/sharedwebapp/Html/Block";
import { MarginCss } from "@jasonbenfield/sharedwebapp/MarginCss";
import { MessageAlertView } from "@jasonbenfield/sharedwebapp/MessageAlertView";
import { TextCss } from '@jasonbenfield/sharedwebapp/TextCss';
import { VerifyLoginFormView } from "../Api/VerifyLoginFormView";

export class LoginComponentView extends Block {
    readonly verifyLoginForm = this.addContent(new VerifyLoginFormView());
    readonly loginButton: ButtonCommandItem;
    readonly alert: MessageAlertView;
    readonly formSubmitted: IEventHandler<any>;

    constructor() {
        super();
        this.verifyLoginForm = this.addContent(new VerifyLoginFormView());
        let commandBlock = this.addContent(new Block());
        commandBlock.addCssFrom(new TextCss().end().cssClass());
        commandBlock.setMargin(MarginCss.bottom(3));
        this.alert = this.addContent(new MessageAlertView());
        this.addCssName("container");
        this.verifyLoginForm.forEachFormGroup(fg => {
            fg.captionColumn.setColumnCss(ColumnCss.xs(3));
        });
        this.verifyLoginForm.addOffscreenSubmit();
        this.formSubmitted = this.verifyLoginForm.submitted;
        this.verifyLoginForm.executeLayout();
        new DelayedAction(() => {
            this.verifyLoginForm.UserName.input.setFocus();
        }, 100).execute();
        this.loginButton = commandBlock.addContent(new ButtonCommandItem());
        this.loginButton.setContext(ContextualClass.primary);
        this.loginButton.setText('Login');
        this.loginButton.icon.solidStyle();
        this.loginButton.icon.setName('sign-in-alt');
    }

    setFocusOnUserName() { this.verifyLoginForm.UserName.input.setFocus(); }
}