// Generated code
import { BaseForm } from '@jasonbenfield/sharedwebapp/Forms/BaseForm';
import { ChangePasswordFormView } from './ChangePasswordFormView';

export class ChangePasswordForm extends BaseForm {
	protected readonly view: ChangePasswordFormView;
	
	constructor(view: ChangePasswordFormView) {
		super('ChangePasswordForm', view);
		this.UserID.setCaption('User ID');
		this.UserID.constraints.mustNotBeNull();
		this.Password.setCaption('Password');
		this.Password.constraints.mustNotBeNull();
		this.Password.constraints.mustNotBeWhitespace('Must not be blank');
		this.Password.protect();
		this.Confirm.setCaption('Confirm');
		this.Confirm.constraints.mustNotBeNull();
		this.Confirm.protect();
	}
	readonly UserID = this.addHiddenNumberFormGroup('UserID', this.view.UserID);
	readonly Password = this.addTextInputFormGroup('Password', this.view.Password);
	readonly Confirm = this.addTextInputFormGroup('Confirm', this.view.Confirm);
}