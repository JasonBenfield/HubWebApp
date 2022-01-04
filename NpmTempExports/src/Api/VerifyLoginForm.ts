// Generated code
import { BaseForm } from '@jasonbenfield/sharedwebapp/Forms/BaseForm';
import { VerifyLoginFormView } from './VerifyLoginFormView';

export class VerifyLoginForm extends BaseForm {
	protected readonly view: VerifyLoginFormView;
	
	constructor(view: VerifyLoginFormView) {
		super('VerifyLoginForm', view);
		this.UserName.setCaption('User Name');
		this.UserName.constraints.mustNotBeNull();
		this.UserName.constraints.mustNotBeWhitespace('Must not be blank');
		this.UserName.setMaxLength(100);
		this.Password.setCaption('Password');
		this.Password.constraints.mustNotBeNull();
		this.Password.constraints.mustNotBeWhitespace('Must not be blank');
		this.Password.setMaxLength(100);
		this.Password.protect();
	}
	readonly UserName = this.addTextInputFormGroup('UserName', this.view.UserName);
	readonly Password = this.addTextInputFormGroup('Password', this.view.Password);
}