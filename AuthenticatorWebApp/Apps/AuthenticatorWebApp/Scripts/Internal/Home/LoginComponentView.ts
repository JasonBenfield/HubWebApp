import { VerifyLoginFormView } from "@hub/Http/VerifyLoginFormView";
import { ContextualClass } from "@jasonbenfield/sharedwebapp/ContextualClass";
import { DelayedAction } from '@jasonbenfield/sharedwebapp/DelayedAction';
import { MarginCss } from "@jasonbenfield/sharedwebapp/MarginCss";
import { TextCss } from '@jasonbenfield/sharedwebapp/TextCss';
import { BasicComponentView } from "@jasonbenfield/sharedwebapp/Views/BasicComponentView";
import { BlockView } from "@jasonbenfield/sharedwebapp/Views/BlockView";
import { ButtonCommandView } from "@jasonbenfield/sharedwebapp/Views/Command";
import { MessageAlertView } from "@jasonbenfield/sharedwebapp/Views/MessageAlertView";

export class LoginComponentView extends BlockView {
    readonly verifyLoginForm: VerifyLoginFormView;
    readonly loginButton: ButtonCommandView;
    readonly alert: MessageAlertView;

    constructor(container: BasicComponentView) {
        super(container);
        this.verifyLoginForm = this.addView(VerifyLoginFormView);
        const commandBlock = this.addView(BlockView);
        commandBlock.addCssFrom(new TextCss().end().cssClass());
        commandBlock.setMargin(MarginCss.bottom(3));
        this.alert = this.addView(MessageAlertView);
        this.addCssName("container");
        this.verifyLoginForm.addOffscreenSubmit();
        this.verifyLoginForm.addContent();
        new DelayedAction(() => {
            this.verifyLoginForm.UserName.input.setFocus();
        }, 100).execute();
        this.loginButton = commandBlock.addView(ButtonCommandView);
        this.loginButton.setContext(ContextualClass.primary);
        this.loginButton.setText('Login');
        this.loginButton.icon.solidStyle('sign-in-alt');
    }

    setFocusOnUserName() { this.verifyLoginForm.UserName.input.setFocus(); }
}