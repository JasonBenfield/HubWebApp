// Generated code
import { BaseFormView } from '@jasonbenfield/sharedwebapp/Views/BaseFormView';
import * as views from '@jasonbenfield/sharedwebapp/Views/FormGroup';
import { IFormGroupLayout } from '@jasonbenfield/sharedwebapp/Views/Types';
import { BasicComponentView } from '@jasonbenfield/sharedwebapp/Views/BasicComponentView';
import { InputView } from '@jasonbenfield/sharedwebapp/Views/InputView';

export interface IEditUserFormView {
	UserID: InputView;
	PersonName: views.SimpleFieldFormGroupInputView;
	Email: views.SimpleFieldFormGroupInputView;
}

export class DefaultEditUserFormViewLayout implements IFormGroupLayout<IEditUserFormView> {
	addFormGroups(form: EditUserFormView) {
		return {
			UserID: form.addHiddenInput(),
			PersonName: form.addInputFormGroup(),
			Email: form.addInputFormGroup()
		}
	}
}

export class EditUserFormView extends BaseFormView {
	private formGroups: IEditUserFormView;
	
	constructor(container: BasicComponentView) {
		super(container);
	}
	
	addContent(layout?: IFormGroupLayout<IEditUserFormView>) {
		if (!layout) {
			layout = new DefaultEditUserFormViewLayout();
		}
		this.formGroups = layout.addFormGroups(this);
	}
	
	get UserID() { return this.formGroups.UserID; }
	get PersonName() { return this.formGroups.PersonName; }
	get Email() { return this.formGroups.Email; }
}