// Generated code
import { BaseFormView } from '@jasonbenfield/sharedwebapp/Views/BaseFormView';
import { SimpleFieldFormGroupInputView, SimpleFieldFormGroupSelectView } from '@jasonbenfield/sharedwebapp/Views/FormGroup';
import { IFormGroupLayout } from '@jasonbenfield/sharedwebapp/Views/Types';
import { BasicComponentView } from '@jasonbenfield/sharedwebapp/Views/BasicComponentView';

export interface IEditUserFormView {
	UserID: SimpleFieldFormGroupInputView;
	PersonName: SimpleFieldFormGroupInputView;
	Email: SimpleFieldFormGroupInputView;
}

export class DefaultEditUserFormViewLayout implements IFormGroupLayout<IEditUserFormView> {
	addFormGroups(form: EditUserFormView) {
		return {
			UserID: form.addInputFormGroup(),
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