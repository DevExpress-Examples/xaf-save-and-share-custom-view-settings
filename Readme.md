<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128592707/17.1.5%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/T537863)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
[![](https://img.shields.io/badge/💬_Leave_Feedback-feecdd?style=flat-square)](#does-this-example-address-your-development-requirementsobjectives)
<!-- default badges end -->
<!-- default file list -->
*Files to look at*:

* [SettingsStore.cs](./CS/ViewSettingsSolution.Module/BusinessObjects/SettingsStore.cs) (VB: [SettingsStore.vb](./VB/ViewSettingsSolution.Module/BusinessObjects/SettingsStore.vb))
* **[ViewVariantsController.cs](./CS/ViewSettingsSolution.Module/Controllers/ViewVariantsController.cs) (VB: [ViewVariantsController.vb](./VB/ViewSettingsSolution.Module/Controllers/ViewVariantsController.vb))**
<!-- default file list end -->
# How to save and share custom view settings


<p>In XAF applications, View settings are stored in Model Differences individually for each user. When the user changes the View (e. g., adds a column to a List View or groups View Items in a Detail View layout), these settings are saved in the user's Model Differences and applied to this View the next time it is displayed.<br>The built-in <a href="https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113011.aspx">View Variants Module</a> allows creating multiple predefined view settings using the Model Editor or in code and provides the end user with the capability to select a view variant at runtime.</p>
<p> </p>
<p><strong>Scenario</strong></p>
<p>We want to provide the capability to save customized view settings as new view variants at runtime, share them, and allow any user having the corresponding permission to select a variant and apply these settings.</p>
<p> </p>
<p><strong>Solution</strong></p>
<p>In this example, we use a persistent class to store view settings in the database and a view controller with actions to manage these settings (creating, applying and deleting). Each user can create his/her own view variants. Each view variant can be optionally marked as shared, so that other users can see and apply it to their views.</p>
<p><br>First, create the <strong>SettingsStore </strong>class with the following properties to store the View settings:<br>1. <strong>Xml</strong> - a string where serialized view settings are stored;<br>2. <strong>Name</strong> - the name of the View Variant;<br>3. <strong>OwnerId</strong> - an identifier of the user who created this Variant;<br>4. <strong>IsShared</strong> - specifies whether this Variant is shared with other users, or not;<br>5. <strong>ViewId </strong>- an identifier of the View for which this variant is created.</p>
<p> </p>
<p>Then, create the <strong>ViewController</strong> with the following behavior:<br>1. There are Shared Model Settings available for each user, which cannot be edited by them.<br>2. Each user has his/her own default settings saved in the user's Model and used when there are no variants.<br>3. The <strong>SaveAsNewViewVariant</strong> action creates a new View Variant based on current view customizations. If this is the first Variant created for the view, two new Variants are created: a Variant that stores default settings (named "Default") and a Variant that stores customized view settings. If at least one Variant already exists, only the latter View Variant (with current customizations) is created. This variant becomes current. <br>4. The <strong>SelectViewVariant</strong> action makes the View Variant selected in the combo box current. This action is available when at least one variant exists. When the current View Variant is changed, customizations applied to the previous View Variant are lost. Only the "Default" View Variant customizations are saved in the Model when the current variant is changed.<br>5. The <strong>UpdateCurrentViewVariant</strong> action saves customizations to the currently selected View Variant.<br>6. The <strong>DeleteViewVariant</strong> action deletes the current View Variant. After deletion, the "Default" view variant becomes current and its settings are applied.<br>7. The <strong>UpdateDefaultViewVariant</strong> action saves customizations made to the current view in the "Default" Variant.</p>
<p> </p>
<p>Actions provided by the <strong>ViewVariantsController</strong> controller may look as follows:</p>
<p><img src="https://raw.githubusercontent.com/DevExpress-Examples/how-to-save-and-share-custom-view-settings-t537863/17.1.5+/media/d55bdb03-4862-452a-b4e3-feb56a5c3abf.png"></p>
<p><br>This example demonstrates the basic functionality, which you can expand or customize according to your requirements. For example, you can prevent certain users from deleting View Variants using the <a href="https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113361.aspx">Security System</a> facilities. Also, you can store the current Variant in the Model (see the <a href="https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113169.aspx">Extend and Customize the Application Model in Code</a> topic in our documentation) or in a property of the user object and apply it when the corresponding View is opened.</p>
<p>Note that the approach demonstrated by this example may be inappropriate or too complicated for certain use cases. So, we cannot guarantee that it will work in all possible scenarios. Should you face any issue using this solution or difficulty implementing it in your project, please submit a separate ticket and describe your scenario in detail. We will research this information to make the functionality better.<br><br></p>

<br/>


<!-- feedback -->
## Does this example address your development requirements/objectives?

[<img src="https://www.devexpress.com/support/examples/i/yes-button.svg"/>](https://www.devexpress.com/support/examples/survey.xml?utm_source=github&utm_campaign=xaf-save-and-share-custom-view-settings&~~~was_helpful=yes) [<img src="https://www.devexpress.com/support/examples/i/no-button.svg"/>](https://www.devexpress.com/support/examples/survey.xml?utm_source=github&utm_campaign=xaf-save-and-share-custom-view-settings&~~~was_helpful=no)

(you will be redirected to DevExpress.com to submit your response)
<!-- feedback end -->
