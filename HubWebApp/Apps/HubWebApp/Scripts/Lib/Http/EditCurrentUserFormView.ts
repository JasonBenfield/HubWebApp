// Generated code
import { BaseFormView } from '@jasonbenfield/sharedwebapp/Views/BaseFormView';
import { SimpleFieldFormGroupInputView, SimpleFieldFormGroupSelectView } from '@jasonbenfield/sharedwebapp/Views/FormGroup';
import { IFormGroupLayout } from '@jasonbenfield/sharedwebapp/Views/Types';
import { BasicComponentView } from '@jasonbenfield/sharedwebapp/Views/BasicComponentView';

export interface IEditCurrentUserFormView {
	PersonName: SimpleFieldFormGroupInputView;
	Email: SimpleFieldFormGroupInputView;
}

export class DefaultEditCurrentUserFormViewLayout implements IFormGroupLayout<IEditCurrentUserFormView> {
	addFormGroups(form: EditCurrentUserFormView) {
		return {
			PersonName: form.addInputFormGroup(),
			Email: form.addInputFormGroup()
		}
	}
}

export class EditCurrentUserFormView extends BaseFormView {
	private formGroups: IEditCurrentUserFormView;
	
	constructor(container: BasicComponentView) {
		super(container);
	}
	
	addContent(layout?: IFormGroupLayout<IEditCurrentUserFormView>) {
		if (!layout) {
			layout = new DefaultEditCurrentUserFormViewLayout();
		}
		this.formGroups = layout.addFormGroups(this);
	}
	
	get PersonName() { return this.formGroups.PersonName; }
	get Email() { return this.formGroups.Email; }
}