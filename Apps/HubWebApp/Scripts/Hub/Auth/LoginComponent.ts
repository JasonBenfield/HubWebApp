import { AsyncCommand } from "XtiShared/Command/AsyncCommand";
import { ColumnCss } from "XtiShared/ColumnCss";
import { UrlBuilder } from 'XtiShared/UrlBuilder';
import { VerifyLoginForm } from "../Api/VerifyLoginForm";
import { DelayedAction } from 'XtiShared/DelayedAction';
import { BlockViewModel } from "XtiShared/Html/BlockViewModel";
import { Block } from "XtiShared/Html/Block";
import { MessageAlert } from 'XtiShared/MessageAlert';
import { TextCss } from 'XtiShared/TextCss';
import { MarginCss } from "XtiShared/MarginCss";
import { ButtonCommandItem } from "XtiShared/Command/ButtonCommandItem";
import { ContextualClass } from "XtiShared/ContextualClass";
import { HubAppApi } from "../Api/HubAppApi";

export class LoginResult {
    constructor(public readonly token: string) {
    }
}

export class LoginComponent extends Block {
    public static readonly ResultKeys = {
        loginComplete: 'login-complete'
    };

    constructor(
        private readonly authApi: HubAppApi,
        vm: BlockViewModel = new BlockViewModel()
    ) {
        super(vm);
        this.addCssName("container");
        this.verifyLoginForm.forEachFormGroup(fg => {
            fg.captionColumn.setColumnCss(ColumnCss.xs(3));
        });
        this.verifyLoginForm.addOffscreenSubmit();
        this.verifyLoginForm.submitted.register(this.onSubmit.bind(this));
        this.verifyLoginForm.executeLayout();
        new DelayedAction(() => {
            this.verifyLoginForm.UserName.setFocus();
        }, 100).execute();
        let loginButton = this.loginCommand.add(
            this.commandBlock.addContent(new ButtonCommandItem())
        );
        loginButton.setContext(ContextualClass.primary);
        loginButton.setText('Login');
        loginButton.icon.solidStyle();
        loginButton.icon.setName('sign-in-alt');
    }

    private onSubmit() {
        return this.loginCommand.execute();
    }

    private readonly verifyLoginForm = this.addContent(new VerifyLoginForm());
    private readonly loginCommand = new AsyncCommand(this.login.bind(this));
    private readonly commandBlock = this.addContent(new Block())
        .configure(b => {
            b.addCssFrom(new TextCss().end().cssClass());
            b.setMargin(MarginCss.bottom(3));
        });
    private readonly alert = this.addContent(new MessageAlert());

    private async login() {
        this.alert.info('Verifying login...');
        try {
            let result = await this.verifyLoginForm.save(this.authApi.Auth.VerifyLoginAction);
            if (result.succeeded()) {
                let cred = this.getCredentials();
                this.alert.info('Opening page...');
                this.postLogin(cred);
            }
        }
        finally {
            this.alert.clear();
        }
    }

    private getCredentials() {
        return <ILoginCredentials>{
            UserName: this.verifyLoginForm.UserName.getValue(),
            Password: this.verifyLoginForm.Password.getValue()
        };
    }

    private postLogin(cred: ILoginCredentials) {
        let form = <HTMLFormElement>document.createElement('form');
        form.action = this.authApi.Auth.Login
            .getUrl(null)
            .value();
        form.style.position = 'absolute';
        form.style.top = '-100px';
        form.style.left = '-100px';
        form.method = 'POST';
        let userNameInput = this.createInput('Credentials.UserName', cred.UserName, 'text');
        let passwordInput = this.createInput('Credentials.Password', cred.Password, 'password');
        let urlBuilder = UrlBuilder.current();
        let startUrlInput = this.createInput('StartUrl', urlBuilder.getQueryValue('startUrl'));
        let returnUrlInput = this.createInput('ReturnUrl', urlBuilder.getQueryValue('returnUrl'));
        form.append(
            userNameInput,
            passwordInput,
            startUrlInput,
            returnUrlInput
        );
        document.body.append(form);
        form.submit();
    }

    private createInput(name: string, value: string, type: string = 'hidden') {
        let input = <HTMLInputElement>document.createElement('input');
        input.type = type;
        input.name = name;
        input.value = value;
        return input;
    }
}