// Generated code
import { BaseFormView } from '@jasonbenfield/sharedwebapp/Views/BaseFormView';
import * as views from '@jasonbenfield/sharedwebapp/Views/FormGroup';
import { IFormGroupLayout } from '@jasonbenfield/sharedwebapp/Views/Types';
import { BasicComponentView } from '@jasonbenfield/sharedwebapp/Views/BasicComponentView';
import { InputView } from '@jasonbenfield/sharedwebapp/Views/InputView';

export interface IChangePasswordFormView {
	UserID: InputView;
	Password: views.SimpleFieldFormGroupInputView;
	Confirm: views.SimpleFieldFormGroupInputView;
}

export class DefaultChangePasswordFormViewLayout implements IFormGroupLayout<IChangePasswordFormView> {
	addFormGroups(form: ChangePasswordFormView) {
		return {
			UserID: form.addHiddenInput(),
			Password: form.addInputFormGroup(),
			Confirm: form.addInputFormGroup()
		}
	}
}

export class ChangePasswordFormView extends BaseFormView {
	private formGroups: IChangePasswordFormView;
	
	constructor(container: BasicComponentView) {
		super(container);
	}
	
	addContent(layout?: IFormGroupLayout<IChangePasswordFormView>) {
		if (!layout) {
			layout = new DefaultChangePasswordFormViewLayout();
		}
		this.formGroups = layout.addFormGroups(this);
	}
	
	get UserID() { return this.formGroups.UserID; }
	get Password() { return this.formGroups.Password; }
	get Confirm() { return this.formGroups.Confirm; }
}