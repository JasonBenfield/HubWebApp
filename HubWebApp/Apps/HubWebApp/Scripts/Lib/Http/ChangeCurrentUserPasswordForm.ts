// Generated code
import * as xti from "@jasonbenfield/sharedwebapp/Common";
import { BaseForm } from '@jasonbenfield/sharedwebapp/Forms/BaseForm';
import { ChangeCurrentUserPasswordFormView } from './ChangeCurrentUserPasswordFormView';

export class ChangeCurrentUserPasswordForm extends BaseForm {
	constructor(protected readonly view: ChangeCurrentUserPasswordFormView) {
		super('ChangeCurrentUserPasswordForm', view);
		this.Password.setCaption('Password');
		this.Password.constraints.mustNotBeNull();
		this.Password.constraints.mustNotBeWhitespace('Must not be blank');
		this.Password.protect();
		this.Confirm.setCaption('Confirm');
		this.Confirm.constraints.mustNotBeNull();
		this.Confirm.protect();
	}
	get Password() { return this.addTextInputFormGroup('Password', this.view.Password); };
	get Confirm() { return this.addTextInputFormGroup('Confirm', this.view.Confirm); };
}