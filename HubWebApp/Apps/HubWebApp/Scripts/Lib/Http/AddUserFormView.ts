// Generated code
import { BaseFormView } from '@jasonbenfield/sharedwebapp/Views/BaseFormView';
import * as views from '@jasonbenfield/sharedwebapp/Views/FormGroup';
import { IFormGroupLayout } from '@jasonbenfield/sharedwebapp/Views/Types';
import { BasicComponentView } from '@jasonbenfield/sharedwebapp/Views/BasicComponentView';
import { InputView } from '@jasonbenfield/sharedwebapp/Views/InputView';

export interface IAddUserFormView {
	UserName: views.SimpleFieldFormGroupInputView;
	Password: views.SimpleFieldFormGroupInputView;
	Confirm: views.SimpleFieldFormGroupInputView;
	PersonName: views.SimpleFieldFormGroupInputView;
	Email: views.SimpleFieldFormGroupInputView;
}

export class DefaultAddUserFormViewLayout implements IFormGroupLayout<IAddUserFormView> {
	addFormGroups(form: AddUserFormView) {
		return {
			UserName: form.addInputFormGroup(),
			Password: form.addInputFormGroup(),
			Confirm: form.addInputFormGroup(),
			PersonName: form.addInputFormGroup(),
			Email: form.addInputFormGroup()
		}
	}
}

export class AddUserFormView extends BaseFormView {
	private formGroups: IAddUserFormView;
	
	constructor(container: BasicComponentView) {
		super(container);
	}
	
	addContent(layout?: IFormGroupLayout<IAddUserFormView>) {
		if (!layout) {
			layout = new DefaultAddUserFormViewLayout();
		}
		this.formGroups = layout.addFormGroups(this);
	}
	
	get UserName() { return this.formGroups.UserName; }
	get Password() { return this.formGroups.Password; }
	get Confirm() { return this.formGroups.Confirm; }
	get PersonName() { return this.formGroups.PersonName; }
	get Email() { return this.formGroups.Email; }
}