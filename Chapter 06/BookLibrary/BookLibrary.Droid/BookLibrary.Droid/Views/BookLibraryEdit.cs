#pragma warning disable 1591
// ------------------------------------------------------------------------------
//  <autogenerated>
//      This code was generated by a tool.
//      Mono Runtime Version: 4.0.30319.42000
// 
//      Changes to this file may cause incorrect behavior and will be lost if 
//      the code is regenerated.
//  </autogenerated>
// ------------------------------------------------------------------------------

namespace BookLibrary.Droid
{
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#line 1 "BookLibraryEdit.cshtml"
using BookLibrary.Droid;

#line default
#line hidden


[System.CodeDom.Compiler.GeneratedCodeAttribute("RazorTemplatePreprocessor", "2.6.0.0")]
public partial class BookLibraryEdit : BookLibraryEditBase
{

#line hidden

#line 2 "BookLibraryEdit.cshtml"
public BookItem Model { get; set; }

#line default
#line hidden


public override void Execute()
{
WriteLiteral("<html>\n<head>\n\t<link");

WriteLiteral(" rel=\"stylesheet\"");

WriteLiteral(" href=\"style.css\"");

WriteLiteral(" />\n</head>\n<body>\n\t<h1>Edit Book Details</h1>\n\t<table");

WriteLiteral(" border=\"1\"");

WriteLiteral(" cellpadding=\"8\"");

WriteLiteral(">\n\t<form");

WriteLiteral(" action=\"hybrid:SaveBookDetails\"");

WriteLiteral(" method=\"GET\"");

WriteLiteral(">\n\t<input");

WriteLiteral(" name=\"id\"");

WriteLiteral(" type=\"hidden\"");

WriteAttribute ("value", " value=\"", "\""

#line 11 "BookLibraryEdit.cshtml"
   , Tuple.Create<string,object,bool> ("", Model.id

#line default
#line hidden
, false)
);
WriteLiteral(" />\n\t\t<tr>\n\t\t<td>Title:\n\t\t<input");

WriteLiteral(" name=\"Title\"");

WriteAttribute ("value", " value=\"", "\""

#line 14 "BookLibraryEdit.cshtml"
, Tuple.Create<string,object,bool> ("", Model.title

#line default
#line hidden
, false)
);
WriteLiteral(" /></td>\n\t\t<tr>\n\t\t<td>Author:\n\t\t<input");

WriteLiteral(" name=\"Author\"");

WriteAttribute ("value", " value=\"", "\""

#line 17 "BookLibraryEdit.cshtml"
, Tuple.Create<string,object,bool> ("", Model.author

#line default
#line hidden
, false)
);
WriteLiteral(" /></td>\n\t\t<tr>\n\t\t<td>Book ISBN:\n\t\t<input");

WriteLiteral(" name=\"ISBN\"");

WriteAttribute ("value", " value=\"", "\""

#line 20 "BookLibraryEdit.cshtml"
, Tuple.Create<string,object,bool> ("", Model.isbn

#line default
#line hidden
, false)
);
WriteLiteral(" /></td>\n\t\t<tr>\n\t\t<td>Synopsis:\n\t\t<textarea");

WriteLiteral(" name=\"Synopsis\"");

WriteLiteral(" rows=\"5\"");

WriteLiteral(" cols=\"40\"");

WriteLiteral(">\n");

WriteLiteral("   \t\t");


#line 24 "BookLibraryEdit.cshtml"
   Write(Model.synopsis);


#line default
#line hidden
WriteLiteral("\n   \t\t</textarea></td>\n\t\t<tr>\n\t\t<td");

WriteLiteral(" colspan=\"8\"");

WriteLiteral(">\n\t\t<input");

WriteLiteral(" type=\"submit\"");

WriteLiteral(" name=\"Button\"");

WriteLiteral(" value=\"Save\"");

WriteLiteral("/>\n\t\t<input");

WriteLiteral(" type=\"submit\"");

WriteLiteral(" name=\"Button\"");

WriteLiteral(" value=\"Cancel\"");

WriteLiteral("/>\n");


#line 30 "BookLibraryEdit.cshtml"
		

#line default
#line hidden

#line 30 "BookLibraryEdit.cshtml"
         if (Model.id > 0) {


#line default
#line hidden
WriteLiteral("\t\t<input");

WriteLiteral(" type=\"submit\"");

WriteLiteral(" name=\"Button\"");

WriteLiteral(" value=\"Delete\"");

WriteLiteral("/>\n");


#line 32 "BookLibraryEdit.cshtml"
		}


#line default
#line hidden
WriteLiteral("\t\t</td>\n\t\t</tr>\n\t</form>\n</table>\n</body>\n</html>");

}
}

// NOTE: this is the default generated helper class. You may choose to extract it to a separate file 
// in order to customize it or share it between multiple templates, and specify the template's base 
// class via the @inherits directive.
public abstract class BookLibraryEditBase
{

		// This field is OPTIONAL, but used by the default implementation of Generate, Write, WriteAttribute and WriteLiteral
		//
		System.IO.TextWriter __razor_writer;

		// This method is OPTIONAL
		//
		/// <summary>Executes the template and returns the output as a string.</summary>
		/// <returns>The template output.</returns>
		public string GenerateString ()
		{
			using (var sw = new System.IO.StringWriter ()) {
				Generate (sw);
				return sw.ToString ();
			}
		}

		// This method is OPTIONAL, you may choose to implement Write and WriteLiteral without use of __razor_writer
		// and provide another means of invoking Execute.
		//
		/// <summary>Executes the template, writing to the provided text writer.</summary>
		/// <param name="writer">The TextWriter to which to write the template output.</param>
		public void Generate (System.IO.TextWriter writer)
		{
			__razor_writer = writer;
			Execute ();
			__razor_writer = null;
		}

		// This method is REQUIRED, but you may choose to implement it differently
		//
		/// <summary>Writes a literal value to the template output without HTML escaping it.</summary>
		/// <param name="value">The literal value.</param>
		protected void WriteLiteral (string value)
		{
			__razor_writer.Write (value);
		}

		// This method is REQUIRED if the template contains any Razor helpers, but you may choose to implement it differently
		//
		/// <summary>Writes a literal value to the TextWriter without HTML escaping it.</summary>
		/// <param name="writer">The TextWriter to which to write the literal.</param>
		/// <param name="value">The literal value.</param>
		protected static void WriteLiteralTo (System.IO.TextWriter writer, string value)
		{
			writer.Write (value);
		}

		// This method is REQUIRED, but you may choose to implement it differently
		//
		/// <summary>Writes a value to the template output, HTML escaping it if necessary.</summary>
		/// <param name="value">The value.</param>
		/// <remarks>The value may be a Action<System.IO.TextWriter>, as returned by Razor helpers.</remarks>
		protected void Write (object value)
		{
			WriteTo (__razor_writer, value);
		}

		// This method is REQUIRED if the template contains any Razor helpers, but you may choose to implement it differently
		//
		/// <summary>Writes an object value to the TextWriter, HTML escaping it if necessary.</summary>
		/// <param name="writer">The TextWriter to which to write the value.</param>
		/// <param name="value">The value.</param>
		/// <remarks>The value may be a Action<System.IO.TextWriter>, as returned by Razor helpers.</remarks>
		protected static void WriteTo (System.IO.TextWriter writer, object value)
		{
			if (value == null)
				return;

			var write = value as Action<System.IO.TextWriter>;
			if (write != null) {
				write (writer);
				return;
			}

			//NOTE: a more sophisticated implementation would write safe and pre-escaped values directly to the
			//instead of double-escaping. See System.Web.IHtmlString in ASP.NET 4.0 for an example of this.
			writer.Write(System.Net.WebUtility.HtmlEncode (value.ToString ()));
		}

		// This method is REQUIRED, but you may choose to implement it differently
		//
		/// <summary>
		/// Conditionally writes an attribute to the template output.
		/// </summary>
		/// <param name="name">The name of the attribute.</param>
		/// <param name="prefix">The prefix of the attribute.</param>
		/// <param name="suffix">The suffix of the attribute.</param>
		/// <param name="values">Attribute values, each specifying a prefix, value and whether it's a literal.</param>
		protected void WriteAttribute (string name, string prefix, string suffix, params Tuple<string,object,bool>[] values)
		{
			WriteAttributeTo (__razor_writer, name, prefix, suffix, values);
		}

		// This method is REQUIRED if the template contains any Razor helpers, but you may choose to implement it differently
		//
		/// <summary>
		/// Conditionally writes an attribute to a TextWriter.
		/// </summary>
		/// <param name="writer">The TextWriter to which to write the attribute.</param>
		/// <param name="name">The name of the attribute.</param>
		/// <param name="prefix">The prefix of the attribute.</param>
		/// <param name="suffix">The suffix of the attribute.</param>
		/// <param name="values">Attribute values, each specifying a prefix, value and whether it's a literal.</param>
		///<remarks>Used by Razor helpers to write attributes.</remarks>
		protected static void WriteAttributeTo (System.IO.TextWriter writer, string name, string prefix, string suffix, params Tuple<string,object,bool>[] values)
		{
			// this is based on System.Web.WebPages.WebPageExecutingBase
			// Copyright (c) Microsoft Open Technologies, Inc.
			// Licensed under the Apache License, Version 2.0
			if (values.Length == 0) {
				// Explicitly empty attribute, so write the prefix and suffix
				writer.Write (prefix);
				writer.Write (suffix);
				return;
			}

			bool first = true;
			bool wroteSomething = false;

			for (int i = 0; i < values.Length; i++) {
				Tuple<string,object,bool> attrVal = values [i];
				string attPrefix = attrVal.Item1;
				object value = attrVal.Item2;
				bool isLiteral = attrVal.Item3;

				if (value == null) {
					// Nothing to write
					continue;
				}

				// The special cases here are that the value we're writing might already be a string, or that the 
				// value might be a bool. If the value is the bool 'true' we want to write the attribute name instead
				// of the string 'true'. If the value is the bool 'false' we don't want to write anything.
				//
				// Otherwise the value is another object (perhaps an IHtmlString), and we'll ask it to format itself.
				string stringValue;
				bool? boolValue = value as bool?;
				if (boolValue == true) {
					stringValue = name;
				} else if (boolValue == false) {
					continue;
				} else {
					stringValue = value as string;
				}

				if (first) {
					writer.Write (prefix);
					first = false;
				} else {
					writer.Write (attPrefix);
				}

				if (isLiteral) {
					writer.Write (stringValue ?? value);
				} else {
					WriteTo (writer, stringValue ?? value);
				}
				wroteSomething = true;
			}
			if (wroteSomething) {
				writer.Write (suffix);
			}
		}
		// This method is REQUIRED. The generated Razor subclass will override it with the generated code.
		//
		///<summary>Executes the template, writing output to the Write and WriteLiteral methods.</summary>.
		///<remarks>Not intended to be called directly. Call the Generate method instead.</remarks>
		public abstract void Execute ();

}
}
#pragma warning restore 1591
