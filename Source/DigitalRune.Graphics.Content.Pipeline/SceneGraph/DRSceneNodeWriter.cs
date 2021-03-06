﻿// DigitalRune Engine - Copyright (C) DigitalRune GmbH
// This file is subject to the terms and conditions defined in
// file 'LICENSE.TXT', which is part of this source code package.

using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;


namespace DigitalRune.Graphics.Content.Pipeline
{
  /// <summary>
  /// Writes a <see cref="DRSceneNodeContent"/> to binary format that can be read by 
  /// <strong>SceneNodeReader</strong> to load a <strong>SceneNode</strong>.
  /// </summary>
  [ContentTypeWriter]
  public class DRSceneNodeWriter : ContentTypeWriter<DRSceneNodeContent>
  {
    /// <summary>
    /// Gets the assembly qualified name of the runtime target type.
    /// </summary>
    /// <param name="targetPlatform">The target platform.</param>
    /// <returns>The qualified name.</returns>
    public override string GetRuntimeType(TargetPlatform targetPlatform)
    {
      return "DigitalRune.Graphics.SceneGraph.SceneNode, DigitalRune.Graphics, Version=1.2.0.0";
    }


    /// <summary>
    /// Gets the assembly qualified name of the runtime loader for this type.
    /// </summary>
    /// <param name="targetPlatform">Name of the platform.</param>
    /// <returns>Name of the runtime loader.</returns>
    public override string GetRuntimeReader(TargetPlatform targetPlatform)
    {
      return "DigitalRune.Graphics.Content.SceneNodeReader, DigitalRune.Graphics, Version=1.2.0.0";
    }


    /// <summary>
    /// Compiles a strongly typed object into binary format.
    /// </summary>
    /// <param name="output">The content writer serializing the value.</param>
    /// <param name="value">The value to write.</param>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods")]
    protected override void Write(ContentWriter output, DRSceneNodeContent value)
    {
      if (value.Children != null)
      {
        output.Write(value.Children.Count);
        foreach (var child in value.Children)
          output.WriteObject(child);
      }
      else
      {
        output.Write(0);
      }

      output.Write(value.Name ?? string.Empty);
      output.WriteRawObject(value.PoseLocal);
      output.WriteRawObject(value.ScaleLocal);
      output.Write(value.MaxDistance);
      output.WriteSharedResource(value.UserData);
    }
  }
}
